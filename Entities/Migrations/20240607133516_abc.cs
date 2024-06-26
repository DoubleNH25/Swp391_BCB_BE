using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRoom",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTarget = table.Column<int>(type: "int", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    settingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    settingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    settingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.settingId);
                });

            migrationBuilder.CreateTable(
                name: "TypePost",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typePost = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePost", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUserRequest = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    acceptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    bankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bankNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccoutName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalRate = table.Column<int>(type: "int", nullable: true),
                    rate = table.Column<double>(type: "float", nullable: true),
                    userRole = table.Column<int>(type: "int", nullable: true),
                    deviceToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayingArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayingLevel = table.Column<int>(type: "int", nullable: false),
                    PlayingWay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBanFromLogin = table.Column<bool>(type: "bit", nullable: false),
                    LogingingDevice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAndroidDevice = table.Column<bool>(type: "bit", nullable: false),
                    isCheckPolicy = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.userRole,
                        principalTable: "Roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_Message_Room",
                        column: x => x.RoomId,
                        principalTable: "ChatRoom",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Message_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<int>(type: "int", nullable: true),
                    ReferenceInfo = table.Column<int>(type: "int", nullable: true),
                    NotiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Notifications",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idType = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    idUserTo = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalViewer = table.Column<int>(type: "int", nullable: false),
                    addressSlot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    levelSlot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    categorySlot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    contentPost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    SlotsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_TypePost",
                        column: x => x.idType,
                        principalTable: "TypePost",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Posts_Users",
                        column: x => x.idUserTo,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    userSubId = table.Column<int>(type: "int", nullable: false),
                    IsSubcription = table.Column<bool>(type: "bit", nullable: false),
                    IsBanded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcribe_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Subcribe_Users1",
                        column: x => x.userSubId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<int>(type: "int", nullable: true),
                    timeTrans = table.Column<DateTime>(type: "datetime", nullable: true),
                    methodTrans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeTrans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    moneyTrans = table.Column<decimal>(type: "money", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserChatRoom",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatRoom", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChatRoomUser_Room",
                        column: x => x.RoomId,
                        principalTable: "ChatRoom",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ChatRoomUser_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "VerifyToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tokens_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<int>(type: "int", nullable: true),
                    balance = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_Wallet_Users",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: true),
                    idPost = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Wishlist_Posts",
                        column: x => x.idPost,
                        principalTable: "Posts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Wishlist_Users",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryTransaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUserFrom = table.Column<int>(type: "int", nullable: true),
                    idUserTo = table.Column<int>(type: "int", nullable: true),
                    idTransaction = table.Column<int>(type: "int", nullable: true),
                    moneyTrans = table.Column<decimal>(type: "money", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTransaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_HistoryTransaction_Transactions",
                        column: x => x.idTransaction,
                        principalTable: "Transactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTransaction = table.Column<int>(type: "int", nullable: true),
                    idUserFrom = table.Column<int>(type: "int", nullable: true),
                    idUserTo = table.Column<int>(type: "int", nullable: true),
                    IdPost = table.Column<int>(type: "int", nullable: true),
                    idRoom = table.Column<int>(type: "int", nullable: true),
                    timeReport = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    reportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.id);
                    table.ForeignKey(
                        name: "FK_Post_Reports",
                        column: x => x.IdPost,
                        principalTable: "Posts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transaction_Reports",
                        column: x => x.IdTransaction,
                        principalTable: "Transactions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledJob",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    ScheduledId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJob", x => x.id);
                    table.ForeignKey(
                        name: "FK_HangfireJob_Transaction",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    slotNumber = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    contentSlot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idPost = table.Column<int>(type: "int", nullable: true),
                    idUser = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.id);
                    table.ForeignKey(
                        name: "FK_Slot_Posts",
                        column: x => x.idPost,
                        principalTable: "Posts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Slot_Transaction",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Slot_Transactions_TransactionId1",
                        column: x => x.TransactionId1,
                        principalTable: "Transactions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Slot_User",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryWallet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idWallet = table.Column<int>(type: "int", nullable: true),
                    idUser = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    time = table.Column<DateTime>(type: "datetime", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryWallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_HistoryWallet_Wallet",
                        column: x => x.idWallet,
                        principalTable: "Wallet",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserRating",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idUserRate = table.Column<int>(type: "int", nullable: true),
                    idUserRated = table.Column<int>(type: "int", nullable: true),
                    time = table.Column<DateTime>(type: "datetime", nullable: true),
                    levelSkill = table.Column<double>(type: "float", nullable: true),
                    friendly = table.Column<double>(type: "float", nullable: true),
                    trusted = table.Column<double>(type: "float", nullable: true),
                    helpful = table.Column<double>(type: "float", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idTransaction = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRating", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRating_HistoryTransaction",
                        column: x => x.idTransaction,
                        principalTable: "HistoryTransaction",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserRating_Users",
                        column: x => x.idUserRated,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTransaction_idTransaction",
                table: "HistoryTransaction",
                column: "idTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryWallet_idWallet",
                table: "HistoryWallet",
                column: "idWallet");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_idType",
                table: "Posts",
                column: "idType");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_idUserTo",
                table: "Posts",
                column: "idUserTo");

            migrationBuilder.CreateIndex(
                name: "IX_Report_IdPost",
                table: "Report",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_Report_IdTransaction",
                table: "Report",
                column: "IdTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJob_TransactionId",
                table: "ScheduledJob",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_idPost",
                table: "Slot",
                column: "idPost");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_idUser",
                table: "Slot",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_TransactionId",
                table: "Slot",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_TransactionId1",
                table: "Slot",
                column: "TransactionId1",
                unique: true,
                filter: "[TransactionId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_userId",
                table: "Subscription",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_userSubId",
                table: "Subscription",
                column: "userSubId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idUser",
                table: "Transactions",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatRoom_RoomId",
                table: "UserChatRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatRoom_UserId",
                table: "UserChatRoom",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_idTransaction",
                table: "UserRating",
                column: "idTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_idUserRated",
                table: "UserRating",
                column: "idUserRated");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userRole",
                table: "Users",
                column: "userRole");

            migrationBuilder.CreateIndex(
                name: "IX_VerifyToken_UserId",
                table: "VerifyToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_idUser",
                table: "Wallet",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_idPost",
                table: "Wishlist",
                column: "idPost");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_idUser",
                table: "Wishlist",
                column: "idUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "HistoryWallet");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "ScheduledJob");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "UserChatRoom");

            migrationBuilder.DropTable(
                name: "UserRating");

            migrationBuilder.DropTable(
                name: "VerifyToken");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "WithdrawDetails");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "ChatRoom");

            migrationBuilder.DropTable(
                name: "HistoryTransaction");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TypePost");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
