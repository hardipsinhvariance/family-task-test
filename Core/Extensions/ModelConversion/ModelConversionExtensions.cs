using Domain.Commands;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ClientSideModels;
using Domain.DataModels;

namespace Core.Extensions.ModelConversion
{
    public static class ModelConversionExtensions
    {
        public static CreateMemberCommand ToCreateMemberCommand(this MemberVm model)
        {
            var command = new CreateMemberCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }

        public static MenuItem[] ToMenuItems(this IEnumerable<MemberVm> models)
        {
            return models.Select(m => new MenuItem()
            {
                iconColor = m.Avatar,
                isActive = false,
                label = $"{m.LastName}, {m.FirstName}",
                referenceId = m.Id
            }).ToArray();
        }

        public static UpdateMemberCommand ToUpdateMemberCommand(this MemberVm model)
        {
            var command = new UpdateMemberCommand()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }
        /// <summary>
        /// Create a task for member
        /// </summary>
        /// <param name="model">model: Task instance</param>
        /// <returns></returns>
        public static CreateTaskCommand ToCreateTaskCommand(this TaskViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            CreateTaskCommand command = new CreateTaskCommand
            {
                Subject = model.Subject,
                AssignedMemberId = model.Member?.Id
            };
            return command;
        }
        /// <summary>
        /// Update a task once completed
        /// </summary>
        /// <param name="model">model: Task instance</param>
        /// <returns></returns>
        public static CompleteTaskCommand ToCompleteTaskCommand(this TaskViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            CompleteTaskCommand command = new CompleteTaskCommand
            {
                Id = model.Id
            };
            return command;
        }
        /// <summary>
        /// Assign a task to particular member.
        /// </summary>
        /// <param name="model">model: Task instance</param>
        /// <returns></returns>
        public static AssignTaskCommand ToAssignTaskCommand(this TaskViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Member?.Id == null)
            {
                throw new ArgumentException("No member found ", nameof(TaskViewModel.Member.Id));
            }
            AssignTaskCommand command = new AssignTaskCommand
            {
                Id = model.Id,
                AssignedMemberId = model.Member.Id
            };
            return command;
        }

        
    }
}
