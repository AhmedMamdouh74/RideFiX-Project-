using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.Enums;

namespace SharedData.DTOs.CarMaintananceDTOs
{
    public class MaintenanceSummaryDTO
    {
        public string MaintenanceTypeName { get; set; } // نوع الصيانة (مثال: الفرامل)
        public MaintenanceEnum Status { get; set; }              // الحالة (Needed, Not Needed Yet, No Information)
        public string? LastMaintenanceDate { get; set; } // آخر صيانة (nullable لو مفيش)
        public string? NextExpectedMaintenance { get; set; } // الصيانة الجاية (nullable لو مفيش)
    }

}
