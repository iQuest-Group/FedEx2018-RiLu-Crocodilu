using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RiluCrocidilu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiluCrocidilu.Hubs
{
    public class ChatHub : Hub
    {
        private readonly RiluCrocodiluContext _context;

        public ChatHub(RiluCrocodiluContext context)
        {
            _context = context;
        }

        public async Task Send(string name, string message)
        {
            if (Clients != null)
            {
                ChatMessage mess = new ChatMessage();

                string[] userName = name.Split(new char[] { ' ' }, 2);
                string fn = userName[1];
                string ln = userName[0];
                var userId = _context.ModuleUser.Where(u => u.FirstName == fn && u.LastName == ln).Select(u => u.UserId).FirstOrDefault();

                mess.Text = message;
                mess.UserId = userId;
                //mess.Timestamp = DateTime.Now;
                _context.ChatMessage.AddRange(mess);

                _context.SaveChanges();

                // Call the addMessage method on all clients
                await Clients.All.SendAsync("addNewMessage", name, message);
                //Clients.All.addNewMessage(name, message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            //string firstName = Context.Request.HttpContext.Session.GetString("Nume");
            //string lastName = Context.Request.HttpContext.Session.GetString("Prenume");
            var name = Context.User.Identity.Name;
            var user = await _context.ModuleUser.Where(u => u.Email == name).FirstOrDefaultAsync();
            if(user != null)
            {
                OnLineUser.AddUser(connectionId, user, user.UserId);

                var loggedUser = _context.LoggedInUsers.Where(u => u.UserId == user.UserId).FirstOrDefault();
                if (loggedUser != null)
                {
                    loggedUser.ConnectionId = connectionId;
                    _context.LoggedInUsers.Update(loggedUser);
                }
                else
                {
                    LoggedInUsers newLUser = new LoggedInUsers();
                    newLUser.ConnectionId = connectionId;
                    newLUser.UserId = user.UserId;
                    _context.LoggedInUsers.AddRange(newLUser);
                }

                var userName = user.LastName + " " + user.FirstName;
                await Clients.Caller.SendAsync("onConnected", user.UserId, connectionId, userName, OnLineUser.onLineUserList);
                await Clients.AllExcept(connectionId).SendAsync("newUserConnected", user.UserId, userName, OnLineUser.onLineUserList);
                //Clients.Caller.onConnected(user.UserID, connectionId, userName, OnLineUser.onLineUserList);
                //Clients.AllExcept(connectionId).newUserConnected(user.UserID, userName, OnLineUser.onLineUserList);

                _context.SaveChanges();
            }
            
            await base.OnConnectedAsync();
        }

        public override  async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var name = Context.User.Identity.Name;
            var userName = _context.ModuleUser.Where(u => u.Email == name).Select(u => u.LastName + " " + u.FirstName).First();

            var user = _context.LoggedInUsers.Where(u => u.ConnectionId == connectionId).FirstOrDefault();
            if (user != null)
            {
                user.ConnectionId = null;
                _context.LoggedInUsers.Update(user);
                _context.SaveChanges();
                OnLineUser.RemoveUser(connectionId, user.UserId);

                await Clients.AllExcept(connectionId).SendAsync("onUserDisconnected", user.UserId, userName, OnLineUser.onLineUserList);
                //Clients.AllExcept(connectionId).onUserDisconnected(user.UserId, userName, OnLineUser.onLineUserList);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task CreateGroup(int currentUserId, int toConnectTo)
        {
            string strGroupName = GetUniqueGroupName(currentUserId, toConnectTo);
            string connectionId_To = OnLineUser.onLineUserList.Where(item => item.UserId == toConnectTo).Select(item => item.ConnectionId).SingleOrDefault();
            string name = OnLineUser.onLineUserList.Where(item => item.UserId == toConnectTo).Select(item => item.User.LastName + " " + item.User.FirstName).First();
            if (!string.IsNullOrEmpty(connectionId_To))
            {
                PrivateMessageHistory pmh = new PrivateMessageHistory(_context, currentUserId, toConnectTo);

                await Groups.AddToGroupAsync(Context.ConnectionId, strGroupName);
                await Groups.AddToGroupAsync(connectionId_To, strGroupName);
                await Clients.Caller.SendAsync("setChatWindow", strGroupName, toConnectTo, name, pmh.GetMessages());
                //Clients.Caller.setChatWindow(strGroupName, toConnectTo, name, pmh.GetMessages());
            }
        }

        private string GetUniqueGroupName(int currentUserId, int toConnectTo)
        {
            return (currentUserId.GetHashCode() ^ toConnectTo.GetHashCode()).ToString();
        }

        public async Task SendPrivateMessage(string message, string groupName, int fromUserId, int toUser)
        {
            if (Clients != null)
            {
                PrivateMessageHistory pmh = new PrivateMessageHistory(_context, fromUserId, toUser);
                var userInfo = pmh.GetMessages();

                PrivateMessage privateMessage = new PrivateMessage();
                privateMessage.Text = message;
                privateMessage.UserId = fromUserId;
                privateMessage.ToUserId = toUser;
                privateMessage.TimeStamp = DateTime.Now;
                _context.PrivateMessage.AddRange(privateMessage);

                _context.SaveChanges();



                var name = _context.ModuleUser.Where(u => u.UserId == fromUserId).Select(u => u.FirstName + " " + u.LastName).First();
                await Clients.Group(groupName).SendAsync("addMessage", message, groupName, fromUserId, toUser, name, userInfo);
                // Clients.Group(groupName).addMessage(message, groupName, fromUserId, toUser, name, userInfo);
            }
        }

        public static class OnLineUser
        {
            public static List<LoggedInUsers> onLineUserList = new List<LoggedInUsers>();

            public static void AddUser(string connectionId, ModuleUser currentUser, int userId)
            {
                LoggedInUsers user = new LoggedInUsers();
                user.ConnectionId = connectionId;
                user.UserId = userId;
                user.User = currentUser;

                onLineUserList.Add(user);
            }

            public static void RemoveUser(string connectionId, int? userId)
            {
                var user = onLineUserList.Where(u => u.UserId == userId && u.ConnectionId == connectionId).FirstOrDefault();
                if (user != null) onLineUserList.Remove(user);
            }
        }


        public class PrivateMessageHistory
        {
            public class UserInfo
            {
                public string Name { get; set; }
                public string Message { get; set; }

            }

            private RiluCrocodiluContext _context;

            private List<UserInfo> users = new List<UserInfo>();

            private int _currentUserId, _toConnectTo;
            public PrivateMessageHistory(RiluCrocodiluContext context, int currentUserId, int toConnectTo)
            {
                _context = context;
                _currentUserId = currentUserId;
                _toConnectTo = toConnectTo;
            }

            public void AddMessage(string userName, string message)
            {
                UserInfo user = new UserInfo();

                user.Name = userName;
                user.Message = message;
                users.Add(user);
            }

            public List<UserInfo> GetMessages()
            {
                var messages = _context.PrivateMessage.Where(p => (p.UserId == _currentUserId && p.ToUserId == _toConnectTo) || (p.UserId == _toConnectTo && p.ToUserId == _currentUserId)).ToList();
                foreach (var message in messages)
                {
                    AddMessage(_context.ModuleUser.Where(u => u.UserId == message.UserId).Select(u => u.LastName + " " + u.FirstName).First(), message.Text);
                }
                return users;
            }
        }

    }
}
