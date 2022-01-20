$(document).ready(function () {

    $.ajax({

        url: "WebService2.asmx/GetAllFactors",
        method: 'post',
        dataType: "json",

        //contentType: "application/json; charset=utf-8",


        success: function (result) {

            console.log("success");

            var data1 = [];
            var data2 = [];
            var data3 = [];
            var data4 = [];
            var data5 = [];
            var data6 = [];
            var data7 = [];
            var data8 = [];

            var labels = [];
            // var borderColor = [];
            var backgroundColor = [];
            // var red, green, blue;

            const ctx = document.getElementById('chartHours').getContext('2d');

            if (result) {

                for (var i = 0; i < result.length; i++) {
                    if (result[i]) {



                        data1[i] = result[i].FwoBelts;
                        data2[i] = result[i].FwAlcohol;
                        data3[i] = result[i].FwBelts;
                        data4[i] = result[i].FwoAlcohol;
                        data5[i] = result[i].nFwoBelts;
                        data6[i] = result[i].nFwAlcohol;
                        data7[i] = result[i].nFwBelts;
                        data8[i] = result[i].nFwoAlcohol;
                        labels[i] = result[i].year;
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
                        label: 'Fatality without belts',
                        data: data1,

                        borderColor: "#3e95cd",
                        fill: false
                    },
                    {
                        label: 'Fatality with alcohol',
                        data: data2,
                        borderColor: "#8e5ea2",
                        fill: false
                    },
                    {
                        label: 'Fatality with belts',
                        data: data3,
                        borderColor: "#3cba9f",
                        fill: false
                    },
                    {
                        label: 'Fatality without alcohol',
                        data: data4,
                        borderColor: "#e8c3b9",
                        fill: false
                    },
                    {
                        label: 'No fatality No belts',
                        data: data5,
                        borderColor: "#3e95cd",
                        fill: false
                    },
                    {
                        label: 'No fatality with alcohol',
                        data: data6,
                        borderColor: "#c45850",
                        fill: false
                    },
                    {
                        label: 'No fatality with belts',
                        data: data7,
                        borderColor: "#8E0F6F",
                        fill: false

                    },
                    {
                        label: 'No fatality No alcohol',
                        data: data8,
                        backgroundColor: backgroundColor,
                        borderColor: "#000000",
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