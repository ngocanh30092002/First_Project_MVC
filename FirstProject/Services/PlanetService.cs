using FirstProject.Models;

namespace FirstProject.Services
{
    public class PlanetService: List<PlanetModel>
    {
        public PlanetService() { 
            Add(new PlanetModel()
            {
                Id= 1,
                Name= "Jupiter",
                VnName = "Sao Mộc",
                Content= @"Sao Mộc (khoảng cách đến Mặt Trời 5,2 AU), với khối lượng bằng 318 lần khối lượng Trái Đất và bằng 2,5 lần tổng khối lượng của 7 hành tinh 
                còn lại trong Thái Dương Hệ. Mộc Tinh có thành phần chủ yếu hiđrô và heli. Nhiệt lượng khổng lồ từ bên trong Sao Mộc tạo ra một số đặc trưng bán vĩnh
                cửu trong bầu khí quyển của nó, như các dải mây và Vết đỏ lớn.\r\nSao Mộc có 63 vệ tinh đã biết. 4 vệ tinh lớn nhất, Ganymede, Callisto, Io, và Europa
                (các vệ tinh Galileo) có các đặc trưng tương tự như các hành tinh đá, như núi lửa và nhiệt lượng từ bên trong. Ganymede, vệ tinh tự nhiên lớn nhất trong
                hệ Mặt Trời, có kích thước lớn hơn Sao Thủy."
            });

            Add(new PlanetModel()
            {
                Id = 2,
                Name = "Saturn",
                VnName = "Sao Thổ",
                Content = @"Sao Thổ (khoảng cách đến Mặt Trời 9,5 AU), có đặc trưng khác biệt rõ rệt đó là hệ vành đai kích thước rất lớn, và những đặc điểm giống với 
                Sao Mộc, như về thành phần bầu khí quyển và từ quyển. Mặc dù thể tích của Sao Thổ bằng 60% thể tích của Sao Mộc, nhưng khối lượng của nó chỉ bằng 1/3 
                so với Sao Mộc, hay 95 lần khối lượng Trái Đất, khiến nó trở thành hành tinh có mật độ nhỏ nhất trong hệ Mặt Trời (nhỏ hơn cả mật độ của nước lỏng).
                Vành đai Sao Thổ chứa bụi cũng như các hạt băng và đá nhỏ.\r\nSao Thổ có 62 vệ tinh tự nhiên được xác nhận; 2 trong số đó, Titan và Enceladus, cho thấy
                có các dấu hiệu của hoạt động địa chất, mặc dù đó là các núi lửa băng. Titan, vệ tinh tự nhiên lớn thứ 2 trong Thái Dương Hệ, cũng lớn hơn Sao Thủy và
                là vệ tinh duy nhất trong hệ Mặt Trời có tồn tại 1 bầu khí quyển đáng kể."
            });

            Add(new PlanetModel()
            {
                Id = 3,
                Name = "Uranus",
                VnName = "Sao Thiên Vương",
                Content = @"Sao Thiên Vương (khoảng cách đến Mặt Trời 19,6 AU), khối lượng bằng 14 lần khối lượng Trái Đất, là hành tinh vòng ngoài nhẹ nhất. Trục tự quay
                của nó có đặc trưng lạ thường duy nhất so với các hành tinh khác, độ nghiêng trục quay >900 so với mặt phẳng hoàng đạo. Thiên Vương Tinh có lõi lạnh hơn 
                nhiều so với các hành tinh khí khổng lồ khác và nhiệt lượng bức xạ vào không gian cũng nhỏ. Sao Thiên Vương có 27 vệ tinh tự nhiên đã biết, lớn nhất 
                theo thứ tự từ lớn đến nhỏ là Titania, Oberon, Umbriel, Ariel và Miranda"
            });

            Add(new PlanetModel()
            {
                Id = 4,
                Name = "Neptune",
                VnName = "Sao Hải Vương",
                Content = @"Sao Hải Vương (khoảng cách đến Mặt Trời 30 AU), mặc dù kích cỡ hơi nhỏ hơn Sao Thiên Vương nhưng khối lượng của nó lại lớn hơn (bằng 17 lần
                khối lượng của Trái Đất) và do vậy khối lượng riêng lớn hơn. Nó cũng bức xạ nhiều nhiệt lượng hơn nhưng không lớn bằng của Sao Mộc hay Sao Thổ.Hải Vương
                Tinh có 13 vệ tinh tự nhiên đã biết. Triton là vệ tinh lớn nhất vầ còn sự hoạt động địa chất với các mạch phun nitơ lỏng. Triton cũng là vệ tinh tự nhiên
                duy nhất có qũy đạo nghịch hành. Trên cùng quỹ đạo của Sao Hải Vương cũng có một số hành tinh vi hình (minor planet), gọi là các thiên thể Troia của Sao
                Hải Vương, chúng cộng hưởng quỹ đạo 1:1 với Hải Vương Tinh."
            });

            Add(new PlanetModel()
            {
                Id = 5,
                Name = "Comet",
                VnName = "Sao chổi",
                Content = @"Sao chổi là các vật thể nhỏ trong Thái Dương Hệ, đường kính điển hình chỉ vài kilômét, thành phần chủ yếu là những hợp chất băng dễ bay hơi.
                Chúng có độ lệch tâm quỹ đạo khá lớn, đa phần có điểm cận nhật nằm bên trong quỹ đạo của các hành tinh vòng trong và điểm viễn nhật nằm bên ngoài Pluto.
                Khi 1 sao chổi đi vào vùng hệ Mặt Trời bên trong, do đến gần Mặt Trời hơn làm cho bề mặt băng của nó chuyển tới trạng thái thăng hoa và ion hóa, tạo ra
                một dải bụi và khí dài thoát ra từ nhân sao chổi, hay là đuôi sao chổi, và có thể nhìn thấy bằng mắt thường.\r\n\r\nSao chổi chu kỳ ngắn có chu kỳ nhỏ
                hơn 200 năm. Sao chổi chu kỳ dài có chu kỳ hàng nghìn năm. Sao chổi chu kỳ ngắn được tin là có nguồn gốc từ vành đai Kuiper trong khi các sao chổi chu
                kỳ dài như Hale-Bopp, nó được cho là có nguồn gốc từ đám mây Oort. Nhiều nhóm sao chổi, như nhóm sao chổi Kreutz, hình thành từ sự tách vỡ của sao chổi
                lớn hơn. Một số sao chổi có quỹ đạo hyperbol có nguồn gốc từ ngoài Hệ Mặt Trời và vấn đề xác định chu kỳ quỹ đạo chính xác của chúng là việc khó khăn. 
                Một số sao chổi trước đây có các chất dễ bay hơi ở bề mặt bị thổi ra ngoài bởi gió Mặt Trời ấm được xếp loại vào tiểu hành tinh."
            });
           
        }
    }
}
