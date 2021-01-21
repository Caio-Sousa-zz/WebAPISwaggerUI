(function () {
    window.addEventListener("load", function ()
    {
        setTimeout(function () {
            var logo = document.getElementsByClassName('link'); //For Changing The Link On The Logo Image
            logo[0].href = "https://google.com/";
            logo[0].target = "_blank";
            logo[0].children[0].alt = "TEST";
            logo[0].children[0].src = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/a98e831c-feeb-4f20-9ebf-2523d7f0ee6d/d5hnpcz-5898e8fe-a129-4c92-aeb2-574010c889b4.png/v1/fill/w_900,h_507,q_80,strp/business_logo_example_three_by_jacksouthall_d5hnpcz-fullview.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3siaGVpZ2h0IjoiPD01MDciLCJwYXRoIjoiXC9mXC9hOThlODMxYy1mZWViLTRmMjAtOWViZi0yNTIzZDdmMGVlNmRcL2Q1aG5wY3otNTg5OGU4ZmUtYTEyOS00YzkyLWFlYjItNTc0MDEwYzg4OWI0LnBuZyIsIndpZHRoIjoiPD05MDAifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6aW1hZ2Uub3BlcmF0aW9ucyJdfQ.wBeFKBxAolqFEj2KaahARSsJSFPmqHb7pIDOUVVLaVg"; //For Changing The Logo Image
        });
    });
})();

$("#logo").replaceWith("<span id=\"test\">test</span>");