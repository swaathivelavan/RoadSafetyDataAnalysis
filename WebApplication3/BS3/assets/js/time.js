
$(document).ready(function () {


    //$("#year").find('option:selected').val();
    $('#year').val(2013).trigger("change");

    $("#year").change(function () {
        var year = $("#year").find('option:selected').val();
        console.log(year);


        $.ajax({

            url: "WebService2.asmx/QueryTime",

            method: 'post',
            dataType: "json",
            data: { "year": year },

           // contentType: "application/json; charset=utf-8",


            success: function (result) {

                console.log("success");

                var data = [];


                var labels = [];


                const ctx = document.getElementById('chartHours1').getContext('2d');

                if (result) {

                    for (var i = 0; i < result.length; i++) {
                        if (result[i]) {



                            data[i] = result[i].noofaccidents;

                            labels[i] = result[i].hour;
                        }
                        // red = Math.floor((Math.random() * 255) + 1);
                        // green = Math.floor((Math.random() * 255) + 1);
                        // blue = Math.floor((Math.random() * 255) + 1);


                        // backgroundColor[i] = 'rgba(' + red + ',' + green + ','
                        //     + blue + ', 0.2)';
                        // borderColor[i] = 'rgba(' + red + ',' + green + ',' + blue
                        //     + ', 0.2)';
                    }
                }
                const myChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Number of accidents',
                            data: data,

                            borderColor: "#3e95cd",
                            fill: false
                        }

                        ]
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

    });
});