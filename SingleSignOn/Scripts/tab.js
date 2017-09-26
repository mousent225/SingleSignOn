//TAB
$(document).ready(function() {
    $(".tabs-menu a").click(function(event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".tab-content").not(tab).css("display", "none");
        $(tab).fadeIn();
    });
});



//IN _ OUT
//function fncToggleLeftMenuArea() {
//    if (!leftMenuHide) {
//      jQuery("div#left_wrap").attr('id','left_wrap2');
//      jQuery("div#contents").attr('id','contents2');
//      jQuery("#div_img_toggle_btn").css({
//        "left": 0
//         , "position": "absolute"
//      });
//	  jQuery("#line_map").css({
//        "margin-left": 0
//      });
//	  jQuery("#sub_content").css({
//        "margin-left": 0
//      });
//      jQuery("#img_toggle_btn").attr('src',"img/icon_in.gif");
//      leftMenuHide = true;
//      $('#container').css("background-image", "url()");
//      if (this.fncLeftMenuHide) fncLeftMenuHide();
//    } else {
//      jQuery("div#left_wrap2").attr('id','left_wrap');
//      jQuery("div#contents2").attr('id','contents');
//      jQuery("#div_img_toggle_btn").css({
//        "left": 0
//         , "position": "absolute"
//      });
//	   jQuery("#line_map").css({
//        "margin-left": 45
//      });
//	  jQuery("#sub_content").css({
//        "margin-left": 45
//      });
//      jQuery("#img_toggle_btn").attr('src',"img/icon_out.gif");
//      leftMenuHide = false;
//      $('#container').css("", "");
//      if (this.fncLeftMenuShow) fncLeftMenuShow();
//    }
//  }