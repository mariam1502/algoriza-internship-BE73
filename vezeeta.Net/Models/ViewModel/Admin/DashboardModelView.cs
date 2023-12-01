using Data;

namespace vezeeta.Net.Models.ViewModel.Admin
{
    public class DashboardModelView
    {
        public int NumOfDoctors { get; set; }
        public int NumOfPatients { get; set; }
        public int NumOfRequests { get; set; }
        public int NumOfCompletedRequests { get; set; }
        public IEnumerable<dynamic> Top5Specializations { get; set; }
        public IEnumerable<Data.Doctor> Top10Doctors { get; set; }





    }
}
