    $(function () {
        //JSON.stringify - JSON Serialization
        //contentType - header

        $('#btnNext').click(function () {
            $.ajax({
                url: '/Sudoku/GetSudoku',
                type: "POST",
                data: JSON.stringify(model),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('.draw-table').html(data);
                }
            });
        });
    });
