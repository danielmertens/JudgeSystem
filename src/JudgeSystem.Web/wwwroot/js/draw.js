const map = (value, x1, y1, x2, y2) => (value - x1) * (y2 - x2) / (y1 - x1) + x2;
const offset = 10;
const canvasWidth = 780;
const canvasHeight = 780;
const cicleRadius = 3;

$('document').ready(function () {
    let splitUrl = window.location.href.split('/');
    $.ajax("/visualization/model/" + splitUrl[splitUrl.length-1])
        .done((result) => {
            const gridWidth = result.width - 1;
            const gridHeight = result.height - 1;
            $('#gridWidth')[0].innerText = 'Actual grid width: ' + gridWidth;
            $('#gridHeight')[0].innerText = 'Actual grid height: ' + gridHeight;
            const ctx = $('#myCanvas')[0].getContext('2d');

            for (var i = 0; i < result.rides.length; i++) {
                var ride = result.rides[i];
                switch (ride.state) {
                    case 0:
                        ctx.strokeStyle = "#000000";
                        ctx.fillStyle = "#000000";
                        break;
                    case 1:
                        ctx.strokeStyle = "#00FF00";
                        ctx.fillStyle = "#00FF00";
                        break;
                    case 2:
                        ctx.strokeStyle = "#FF0000";
                        ctx.fillStyle = "#FF0000";
                        break;
                }
                var x1 = map(ride.x1, 0, gridWidth, 0, canvasWidth) + offset;
                var y1 = map(gridHeight - ride.y1, 0, gridHeight, 0, canvasHeight) + offset;
                var x2 = map(ride.x2, 0, gridWidth, 0, canvasWidth) + offset;
                var y2 = map(gridHeight - ride.y2, 0, gridHeight, 0, canvasHeight) + offset;

                ctx.beginPath();
                ctx.moveTo(x1, y1);
                ctx.lineTo(x1, y2);
                ctx.lineTo(x2, y2);
                ctx.stroke();

                ctx.beginPath();
                ctx.arc(x2, y2, cicleRadius, 0, 2 * Math.PI, false);
                ctx.fill();
            }
        });
});
