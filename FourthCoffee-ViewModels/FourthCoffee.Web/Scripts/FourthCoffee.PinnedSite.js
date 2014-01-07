$(function () {
    var faviconPath = "/Content/Images/";
    var externalWindow = null;

    if (window.external.msIsSiteMode()) {
        externalWindow = window.external;
        externalWindow.msSiteModeCreateJumpList("Fourth Coffee!");
        externalWindow.msSiteModeAddJumpListItem("Weekly Specials", "/Specials/Weekly", faviconPath + "favicon-pie.ico");
        externalWindow.msSiteModeAddJumpListItem("Daily Deals", "/Specials/Daily", faviconPath + "favicon-cupcake.ico");
        setTimeout(function () {
            window.external.msSiteModeSetIconOverlay(faviconPath + "favicon-info.ico", "FourthCoffee News");
        }, 5000);
    }
});




