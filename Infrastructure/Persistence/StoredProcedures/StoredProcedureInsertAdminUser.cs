using Infrastructure.Persistence.StoredProcedures;
using System.Text;

namespace Infrastructure.Persistence
{
    public class StoredProcedureInsertAdminUser : StoredProcedureQuery
    {
        public StoredProcedureInsertAdminUser()
        {
            SqlCode = @"USE [TestDb]
						GO

						SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO
						-- =============================================
						-- Author:		Jann Dissoh Solon
						-- Create date: January 19, 2021
						-- Description:	Create Super Admin User
						-- =============================================
						IF NOT EXISTS (SELECT top 1 * FROM [USER] WHERE [ADMIN] = 1)
						BEGIN
							INSERT INTO [USER] VALUES ('0270206e-5b53-4206-93ff-fbb8ad8b93df','Super', 'Admin', 'mailservice199512@gmail.com','sa@2021',1,1)
						END";

        }
    }
}
