<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoPopup.aspx.cs" Inherits="DNNspot.Store.Modules.Catalog.PhotoPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Photo Gallery</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <style type="text/css">
        .photoPopup ul {list-style:none;margin:0;padding:0}
        .photoPopup ul li {float:left; margin-left:3px;margin-right:3px;}
        .photoFooter {clear:both;}

        #photoPopupBody{background-color: #E8E8E8;margin:0;padding:0;}
        img {border:0;}
        #photoControls {width:100%;height:35px;padding:3px 5px 3px 5px;}
	
        #photoPrev {float:left;width:25px;}	
        #photoNext {float:right;width:35px;}
        .photopopupThumbs{padding-top:3px;margin:0 auto;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>


<script type="text/javascript">
    jQuery(document).ready(function () {

        jQuery('.photopopupThumbs ul li a').click(function (event) {
            var imagePopupImage = jQuery(this).attr('href');

            //jQuery('.photoPopupImage').html('<img src="'+imagePopupImage+'"/>');

            jQuery('.photoPopupImage img').attr('src', imagePopupImage);
            jQuery('.photopopupThumbs ul li a').removeClass('active-photo');
            jQuery('img', this).addClass('active-photo');

            //setTimeout(ResetSize(),5000);
            ResetSize();

            event.preventDefault();


        });


        //ResetSize();

    });


    function ResetSize() {
        var img = jQuery('.photoPopupImage img');

        //starter size 600x600

        var bh = 100;
        var bw = 10;

        var currw = jQuery(window).width();
        var currh = jQuery(window).height();

        var imgw = img.width();
        var imgh = img.height();

        var resizew = imgw - currw + bw;
        var resizeh = imgh - currh + bh;

        window.resizeBy(resizew, resizeh);

        document.title = "cw " + currw + " ch" + currh + "imgw: " + imgw + " imgh:" + imgh + "resizew:" + resizew + " rh:" + resizeh;


        //alert(+" "+img.height());   //image size
        //alert(jQuery(document).width()+ " " + jQuery(document).height());  //window size
        //window.resizeBy(300, 300);    //resize,  readjust popup

    }

</script>

</body>
</html>
