using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Trabea.DataAccess.Models;

namespace Trabea.Presentasion.Web.Validations {
    public class ShiftSelectionAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            var dbContext = (TrabeaContext)validationContext.GetService(typeof(TrabeaContext))!;
            var scheduleDate = (DateTime?)validationContext.ObjectInstance.GetType()
                .GetProperty("ScheduleDate")!.GetValue(validationContext.ObjectInstance);
            var shiftId = (long)value!;

            if (scheduleDate.HasValue) {
                //var previousShiftId = shiftId - 1;

                //var hasPreviousShift = dbContext.Schedules
                //    .Any(s => s.ShiftId == previousShiftId && s.ScheduleDate == scheduleDate.Value.Date);

                //if (!hasPreviousShift && previousShiftId > 0) {
                //    return new ValidationResult($"Shift sebelumnya belum dimasukkan. Anda harus memasukkan shift secara berurutan.");
                //}

                var hasDuplicateShift = dbContext.Schedules
                    .Any(s => s.ShiftId == shiftId && s.ScheduleDate == scheduleDate.Value.Date && s.ScheduleDate >= DateTime.Today);

                if (hasDuplicateShift) {
                    return new ValidationResult($"Shift ini sudah dijadwalkan untuk minggu ini.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
