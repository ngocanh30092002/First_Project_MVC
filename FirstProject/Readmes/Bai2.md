[AllowAnonymous]
	-- Không yêu cầu quyền xác thực

[HttpPost("/admin/contact/detail/{id}"), ActionName("Delete")] 
	-- Gọi bằng pthuc post và actionName để truy cập là Delete

[Bind("FullName,Email,Message,PhoneNumber")]
	-- Binding dữ liệu cụ thể