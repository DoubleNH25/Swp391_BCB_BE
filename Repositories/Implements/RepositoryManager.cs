using Entities;
using Entities.Models;
using Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private IHistoryTransactionRepository _historyTransaction;
        private IHistoryWalletRepository _historyWallet;
        private IPostRepository _post;
        private IRoleRepository _role;
        private ISlotRepository _slot;
        private ITransactionRepository _transaction;
        private ITypePostRepository _typePost;
        private IUserRatingRepository _userRating;
        private IUserRepository _user;
        private IWalletRepository _wallet;
        private IVerifyTokenRepository _verifyToken;
        private ISlotPostRepository _SlotPost;


        public RepositoryManager(DataContext context)
        {
            _context = context;
            _historyTransaction = new HistoryTransactionRepository(_context);
            _historyWallet = new HistoryWalletRepository(_context);
            _post = new PostRepository(_context);
            _role = new RoleRepository(_context);
            _slot = new SlotRepository(_context);
            _transaction = new TransactionRepository(_context);
            _typePost = new TypePostRepository(_context);
            _userRating = new UserRatingRepository(_context);
            _user = new UserRepository(_context);
            _wallet = new WalletRepository(_context);
            _verifyToken = new VerifyTokenRepository(_context);
            _SlotPost = new SlotPostRepository(_context);
        }
        public IHistoryTransactionRepository HistoryTransaction
        {
            get
            {
                return _historyTransaction;
            }
        }
        public IHistoryWalletRepository HistoryWallet
        {
            get
            {
                return _historyWallet;
            }
        }
        public IPostRepository Post
        {
            get
            {
                return _post;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                return _role;
            }
        }
        public ISlotRepository Slot
        {
            get
            {
                return _slot;
            }
        }
        public ITransactionRepository Transaction
        {
            get
            {
                return _transaction;
            }
        }
        public ITypePostRepository TypePost
        {
            get
            {
                return _typePost;
            }
        }
        public IUserRatingRepository UserRating
        {
            get
            {
                return _userRating;
            }
        }
        public IUserRepository User
        {
            get
            {
                return _user;
            }
        }
        public IWalletRepository Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public IVerifyTokenRepository VerifyToken
        {
            get
            {
                return _verifyToken;
            }
        }




        public ISlotPostRepository SlotPost
        {
            get
            {
                return _SlotPost;
            }
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
