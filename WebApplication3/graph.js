
$(document).ready(function () {

    $("#vehicletype").change(function () {
        var condition = {};
        condition.vehicletype = $("#vehicletype").find('option:selected').val();

        $.ajax({

            url: 'WebService1.asmx/GetAllColors',
            method: 'post',
            dataType: "json",
            data: '{cmp: ' + JSON.stringify(condition) + '}',
            contentType: "application/json; charset=utf-8",

           
            success: function (data) {


                $(data).each(function (index, result) {


                    $('#color').append("<option value='" + result.color + "'>" + result.color + "</option>");

                });

                var $select = $("#color").selectize();
                var selectize = $select[0].selectize;
            },
            error: function (xhr, err) {
                console.log(xhr.responseText);

                data = xhr.responseText[0];

                $(data).each(function (index, result) {


                    $('#color').append("<option value='" + result.color + "'>" + result.color + "</option>");

                });

                var $select = $("#color").selectize();
                var selectize = $select[0].selectize;
                //alert("error");

            }

        });
    });

 

    $("#GetGraphs").click(function () {

        var vehicletype = $("#vehicletype").find('option:selected').val();
        var color = $("#color").find('option:selected').val();
        var model = $("#model").find('option:selected').val();
        var make = $("#make").find('option:selected').val();
        var year = $("#year").find('option:selected').val();
        var hazmat = $("#hazmat").find('option:selected').val();
        var cvehicle = $("#cvehicle").find('option:selected').val();
        var vehicleId = "";

        var option = {};

        option.vehicletype = vehicletype;
        option.color = color;
        option.model = model;
        option.make = make;
        option.year = year;
        option.hazmat = hazmat;
        option.cvehicle = cvehicle;

        $.ajax({
            url: 'WebService1.asmx/queryOnePartOne',
            method: 'post',
            data: '{cmp: ' + JSON.stringify(option) + '}',
            contentType: "application/json; charset=utf-8",
            success: function (vehicleID) {
                vehicleId = vehicleID.d;
                console.log(vehicleId);



                $.ajax({

                    url: "WebService1.asmx/GetAllSites",
                    method: 'post',
                    dataType: "json",
                    data: { "vehicleId": vehicleId },
                    //contentType: "application/json; charset=utf-8",


                    success: function (result) {

                        console.log("success");

                        var data = [];
                        var labels = [];
                        var borderColor = [];
                        var backgroundColor = [];
                        var red, green, blue;

                       
                        const ctx = document.getElementById('myChart').getContext('2d');
                        
                        if (result) {

                            for (var i = 0; i < result.length; i++) {
                                if (result[i] && result[i].noofaccidents) {
                                    data[i] = result[i].noofaccidents;
                                    labels[i] = result[i].year;
                                }
                                red = Math.floor((Math.random() * 255) + 1);
                                green = Math.floor((Math.random() * 255) + 1);
                                blue = Math.floor((Math.random() * 255) + 1);


                                backgroundColor[i] = 'rgba(' + red + ',' + green + ','
                                    + blue + ', 0.2)';
                                borderColor[i] = 'rgba(' + red + ',' + green + ',' + blue
                                    + ', 0.2)';
                            }
                        }
                        const myChart = new Chart(ctx, {
                            type: 'line',
                            data: {
                                labels: labels,
                                datasets: [{
                                    label: '# of Votes',
                                    data: data,
                                    
                                    borderColor: "#3e95cd",
                                    fill: false
                          
                                }]
                            },
                            options: {
                                scales: {
                                    y: {
                                        beginAtZero: true
                                    }
                                }
                            }
                        });

                        myChart.destroy();


                        //$("#container").append(table);
                    },
                    error: function (xhr, err) {
                        console.log(xhr.responseText);
                        alert("error");
                    }

                });

            },
            error: function (xhr, err) {
                console.log(xhr.responseText);

            }
        });

    /*
        $.ajax({
            url: 'WebService1.asmx/queryOnePartOne',
            type: 'post',
            data: '{cmp: ' + JSON.stringify(option) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log("Success Dashboard Save");

            },
            error: function (xhr, err) {
                console.log(xhr.responseText);
                alert("error");
            }
        });
        */

        
    });


    $.ajax({
        type: "POST",
        url: "WebService1.asmx/GetAllVehicleTypes",
        dataType: "json",
        success: function (data) {


            $(data).each(function (index, result) {


                $('#vehicletype').append("<option value='" + result.vehicletype + "'>" + result.vehicletype + "</option>");

            });

            var $select = $("#vehicletype").selectize();
            var selectize = $select[0].selectize;
        },
        error: function (xhr, err) {
            console.log(xhr.responseText);
            alert("error");

        }

    });

    /*
    $.ajax({
        type: "POST",
        url: "WebService1.asmx/GetAllColors",
        dataType: "json",
        success: function (data) {


            $(data).each(function (index, result) {


                $('#color').append("<option value='" + result.color + "'>" + result.color + "</option>");

            });

            var $select = $("#color").selectize();
            var selectize = $select[0].selectize;
        },
        error: function (xhr, err) {
            console.log(xhr.responseText);
            alert("error");

        }

    });
    */

    $.ajax({
        type: "POST",
        url: "WebService1.asmx/GetAllModels",
        dataType: "json",
        success: function (data) {


            $(data).each(function (index, result) {


                $('#model').append("<option value='" + result.model + "'>" + result.model + "</option>");

            });

            var $select = $("#model").selectize();
            var selectize = $select[0].selectize;
        },
        error: function (xhr, err) {
            console.log(xhr.responseText);
            alert("error");

        }

    });

    $.ajax({
        type: "POST",
        url: "WebService1.asmx/GetAllMakes",
        dataType: "json",
        success: function (data) {


            $(data).each(function (index, result) {


                $('#make').append("<option value='" + result.make + "'>" + result.make + "</option>");

            });

            var $select = $("#make").selectize();
            var selectize = $select[0].selectize;
        },
        error: function (xhr, err) {
            console.log(xhr.responseText);
            alert("error");

        }

    });

    $.ajax({
        type: "POST",
        url: "WebService1.asmx/GetAllYears",
        dataType: "json",
        success: function (data) {


            $(data).each(function (index, result) {


                $('#year').append("<option value='" + result.year + "'>" + result.year + "</option>");

            });

            var $select = $("#year").selectize();
            var selectize = $select[0].selectize;

        },
        error: function (xhr, err) {
            console.log(xhr.responseText);
            alert("error");

        }

    });
    /*
$.ajax({

    url: "WebService1.asmx/GetAllSites",
    method: 'post',
    dataType: "json",
    data: { "vehicleId": vehicleId },
    //contentType: "application/json; charset=utf-8",


    success: function (result) {

        console.log("success");

        var data = [];
        var labels = [];
        var borderColor = [];
        var backgroundColor = [];
        var red, green, blue;

        const ctx = document.getElementById('myChart').getContext('2d');

        if (result) {

            for (var i = 0; i < result.length; i++) {
                if (result[i] && result[i].noofaccidents) {
                    data[i] = result[i].noofaccidents;
                    labels[i] = result[i].year;
                }
                red = Math.floor((Math.random() * 255) + 1);
                green = Math.floor((Math.random() * 255) + 1);
                blue = Math.floor((Math.random() * 255) + 1);


                backgroundColor[i] = 'rgba(' + red + ',' + green + ','
                    + blue + ', 0.2)';
                borderColor[i] = 'rgba(' + red + ',' + green + ',' + blue
                    + ', 0.2)';
            }
        }
        const myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: '# of Votes',
                    data: data,
                    backgroundColor: backgroundColor,
                    borderColor: borderColor,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });


        //$("#container").append(table);
    },
    error: function (xhr, err) {
        console.log(xhr.responseText);
        alert("error");
    }

});



*/
});
