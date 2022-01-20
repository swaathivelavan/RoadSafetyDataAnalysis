

$(document).ready(function () {


    console.log("here");
    $("#menu").on("click", function () {

        console.log("clicked");


        $(".sidebar").toggleClass("change");

        $(".wedotext").toggleClass("change");

        $(".black-text").toggleClass("change");

    });
});


