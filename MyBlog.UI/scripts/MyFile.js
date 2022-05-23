
$(document).ready(function () {

    var $editedText = $("#edited_text");
    var text = $editedText.text();
    $editedText.text(shorten(text, 200, "...", false));



    $('#nav li a').click(function () {
        $('ul li a').removeClass('selected');
        $(this).addClass('selected');
    });
    $('.single-item').slick({
      dots:true,
      dotsClass: "my-dots",
      autoplay: true,
      autoplaySpeed: 2000,
    });


});

function shorten(text, maxLength, delimiter, overflow) {
  delimiter = delimiter || "…";
  overflow = overflow || false;
  var ret = text;
  if (ret.length > maxLength) {
    var breakpoint = overflow ? maxLength + ret.substr(maxLength).indexOf(" ") : ret.substr(0, maxLength).lastIndexOf(" ");
    ret = ret.substr(0, breakpoint) + delimiter;
  }
  return ret;
}
