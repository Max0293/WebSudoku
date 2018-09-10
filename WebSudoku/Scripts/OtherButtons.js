$(function () {
    $('.level-btn').click(function () {
        $.ajax({
            url: '/Sudoku/SelectLevel',
            type: "POST",
            data: JSON.stringify({ level: $(this).data("level") }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.draw-table').html(data);

                PartialScrtipt.InputAction();

                $('#rdraw').show();
                $('.button-level').hide();
            }
        });
    });

    $('#refresh').click(function () {
        $.ajax({
            url: '/Sudoku/Refresh',
            type: "POST",
            data: JSON.stringify(ReservModel.Reserve),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.draw-table').html(data);
                PartialScrtipt.InputAction();
            }
        });

    });
});
