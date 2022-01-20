$(document).ready(function () {

    $.ajax({

        url: "WebService2.asmx/GetAllLegal",
        method: 'post',
        dataType: "json",

        //contentType: "application/json; charset=utf-8",


        success: function (result) {

            console.log("success");

            var data1 = [];
            var data2 = [];
            var data3 = [];
            var data4 = [];
           

            var labels = [];
            // var borderColor = [];
            var backgroundColor = [];
            // var red, green, blue;

            const ctx = document.getElementById('chartHours').getContext('2d');

            if (result) {

                for (var i = 0; i < result.length; i++) {
                    if (result[i]) {



                        data1[i] = result[i].citeaccident;
                        data2[i] = result[i].citenoaccident;
                        data3[i] = result[i].warningaccident;
                        data4[i] = result[i].warningnoaccident;
                        
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
                        label: 'Citation With Accident',
                        data: data1,

                        borderColor: "#3e95cd",
                        fill: false
                    },
                    {
                        label: 'Citation Without Accident',
                        data: data2,
                        borderColor: "#8e5ea2",
                        fill: false
                    },
                    {
                        label: 'Warning With Accident',
                        data: data3,
                        borderColor: "#3cba9f",
                        fill: false
                    },
                    {
                        label: 'Warning Without Accident',
                        data: data4,
                        borderColor: "#e8c3b9",
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