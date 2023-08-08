/*
    Nếu mà truy cập địa chỉ start-here sẽ gọi tới controller First , Action ViewProduct, id = 3
    
    pattern: 
        start-here/{controller = First }/{action = Index}/{id = 1}
        start-here/{controller}/{action}/{*id}
        {url}/{id?} => thỏa mãn tất cả các url vd: abc/1 , gassf/2
    mặc định defaults: 
        controller =>
        action     =>
        ...(tham số) =>

    constraints: // đọc trên document IRouteContrainst https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-3.1#route-constraint-reference

    app.MapControllerRoute(
        name: "first",
        pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id?}",
        defaults: new
        {
            controller = "First",
            action = "ViewProduct"
        },
        constraints: new
        {
            //url = new StringRouteConstraint("xemsanpham"),
            id = new RangeRouteConstraint(2, 4)
        }
    ) ;


    app.MapControllerRoute(
        name: "firstRouter",
        pattern: "firstRouter/{controller}/{action}/{id?}",
        defaults: new { controller = "First", action = "Readme", id = 3}
    );

    /// <summary>
    /// Route(hanhtinh/{id:int}) để thực hiện action thì gọi đến url: hanhtinh/1 
    /// Nếu Action có attribute Route sẽ k chịu ảnh hưởng từ MapControllerRoute
    /// Còn các cái khác vẫn bị ảnh hưởng
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("hanhtinh/{id:int}")] 
    public IActionResult PlanetInfor(int id)
    {
        var planet = _planetService.Where(p => p.Id == id).FirstOrDefault();
        return View("Detail", planet);
    }
 */


 ## Areas
 - là tên dùng để routing
 - là cấu trúc thư mục lưu MVC
 - Tạo cấu trúc thư mục
 
 -- dotnet aspnet-codegenerator area nameArea


 ## Url - phát sinh URl

    - Url.Action()    / 
    - Url.ActionLink() https:localhost::/

        <p>
        @Url.Action()
        </p>

        <p>
            @Url.ActionLink()
        </p>


        <p>
            @Url.Action("Privacy")
        </p>

        <p>
            @Url.Action("Privacy", new{id = 1, name="ngocanh" , hi = "123"})
        </p>



        <p>
            @Url.Action("Neptune", "Planet")
        </p>

        <p>
            @Url.Action("Index", "Product", new {area = "ProductManage"})
        </p>