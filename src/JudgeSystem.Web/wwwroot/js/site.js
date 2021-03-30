// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function fillTable() {
    $.ajax("/api/scores")
        .done((result) => {
            $('#scoreboard tbody').html('');
            for (var i = 0; i < result.length; i++) {
                $('#scoreboard').append(`<tr><td>${i + 1}</td><td><a href="/team/${result[i].id}">${result[i].teamName}</a></td><td>${result[i].score}</td></tr>`)
            }
        });
}

fillTable();

setInterval(fillTable, 10000);
