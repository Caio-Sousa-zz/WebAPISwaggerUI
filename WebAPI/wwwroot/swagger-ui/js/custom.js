(function () {
    window.addEventListener("load", function ()
    {
        setTimeout(function () {
            var logo = document.getElementsByClassName('link'); //For Changing The Link On The Logo Image
            logo[0].href = "https://rashik.com.np/";
            logo[0].target = "_blank";
            logo[0].children[0].alt = "CODTRAN";
            logo[0].children[0].src = "https://blog.rashik.com.np/wp-content/uploads/2020/06/rashikLogo-2-300x113.png"; //For Changing The Logo Image
        });
    });
})();

$("#logo").replaceWith("<span id=\"test\">test</span>");