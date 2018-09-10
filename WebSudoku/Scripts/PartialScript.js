var ReservModel = {
    Reserve: null,
    SaveModel: function (model) {
        ReservModel.Reserve = model;
    }
};

var PartialScrtipt =
    {
        InputAction: function () {
            $(':input').change(function () {
                var i = $(this).data('i');
                var j = $(this).data('j');
                var result = $(this).val();

                if (!$.isNumeric(result)) {
                    $(this).css("background-color", "red");
                }

                if (result == "") {
                    for (var p = 0; p < 3; p++) {
                        for (var o = 0; o < 3; o++) {
                            $('.row-' + (i - (i % 3) + p) + '.column-' + (j - (j % 3) + o)).css("background-color", "#f1f1f1");
                        }
                    }

                    $('.column-' + j).css("background-color", "#f1f1f1");
                    $('.row-' + i).css("background-color", "#f1f1f1");
                    $(this).css("background-color", "yellow");

                    ReservModel.Reserve.Cell[i][j].Data = 0;
                } else {
                    //var currentCell = $(this);
                    //var item = currentCell.val();
                    $.ajax({
                        url: '/Sudoku/Validate',
                        type: "POST",
                        data: JSON.stringify({ i: i, j: j, num: result, sudoku: ReservModel.Reserve }),
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            if (!data.RowValid) {
                                $('.row-' + i).css("background-color", "red");
                            }
                            if (!data.ColumnValid) {
                                $('.column-' + j).css("background-color", "red");
                            }
                            if (!data.BlockValid) {

                                for (var p = 0; p < 3; p++) {
                                    for (var o = 0; o < 3; o++) {
                                        $('.row-' + (i - (i % 3) + p) + '.column-' + (j - (j % 3) + o)).css("background-color", "red");
                                    }
                                }
                            }

                            if (data.RowValid && data.ColumnValid && data.BlockValid) {
                                $('.column-' + j).css("background-color", "#f1f1f1");
                                $('.row-' + i).css("background-color", "#f1f1f1");
                                for (var p = 0; p < 3; p++) {
                                    for (var o = 0; o < 3; o++) {
                                        $('.row-' + (i - (i % 3) + p) + '.column-' + (j - (j % 3) + o)).css("background-color", "#f1f1f1");
                                    }
                                }
                                $('input').css("background-color", "white");
                                ReservModel.SaveModel(data.Sudoku);
                            }
                            else {
                                ReservModel.Reserve.Cell[i][j].Data = 0;
                            }

                            if (data.Final) {
                                $('.draw-table').css("display", "none");
                                $('.congratulation').css("display", "block");
                            }
                        }
                    });
                }
            });
        }
    };
