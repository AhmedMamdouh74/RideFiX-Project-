using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
	public class TechReverseRequestDTO
	{
		public int ReverseRequestId { get; set; }
		public int TechnicianId { get; set; }
		public DateTime TimeStamp { get; set; } 

		public int CarOwnerRequestId { get; set; }
	}
}
