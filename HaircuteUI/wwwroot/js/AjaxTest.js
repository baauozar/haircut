$(function () {

    $('#btnSubmitFaq').on('click', function (e) {

        e.preventDefault(); // prevent default form submission

        // Serialize the form data
        var formData = $('#FaqAdd').serialize();

        $.ajax({
            type: 'POST',
            url: '/Admin/Faq/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('.bd-example-modal-lg').modal('hide');
                
                    location.reload();

                    toastr.success(response.message);

                } else {
                    toastr.error("There was an error");
                }
            },
            error: function (response) {
                console.log("Error: " + response);
            }
        });
    });
    $(".btnEdit").on('click', function () {
        var id = $(this).data('id');
        var quastion = $(this).data('quastion');
        var answer = $(this).data('answer');
        $('#editFaqId').val(id);
        $('#editquastion').val(quastion);
        $('#editAnswer').val(answer); 
    });
    $("#btnEditFaq").on('click', function () {
        var formData = $('#FaqEdit').serialize();  // Serialize the form data

        // Making an AJAX request to submit the edited data
        $.ajax({
            type: 'POST',
            url: '/Admin/Faq/Edit',
            data: formData,
            success: function (response) {
                // Handle the response here
                if (response.success) {
                    toastr.success(response.message);
                    location.reload();  // Reload the page to see updated data
                } else {
                    toastr.error('Error updating the faq');
                }
            },
            error: function (error) {
                console.log('Error updating the brand', error);
            }
        });
    });


    $(document).on('click', '.DeleteFaq', function (e) {
        e.preventDefault();
        var Id = $(this).data('id');  // seçili kapasitenin ID'si alınıyor

        // kullanıcıya silme işlemi için onay mesajı gösteriliyor
        Swal.fire({
            title: 'Emin misiniz?',
            text: "Bu işlemi geri alamayacaksınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!'
        }).then((result) => {
            if (result.isConfirmed) {
                // onay alındıysa AJAX ile silme isteği gönderiliyor
                $.ajax({
                    type: 'POST',
                    url: '/Admin/Faq/Delete',
                    data: { id: Id },
                    success: function (response) {
                        if (response.success) {
                            toastr.success(response.message);  // başarı mesajı gösteriliyor
                            location.reload();  // başarı durumunda sayfa yenileniyor
                        } else {
                            toastr.error('Hata', 'Öğe silinemedi.', 'error');  // hata mesajı gösteriliyor
                        }
                    },
                    error: function (xhr, status, error) {

                        console.log('Hata', 'Öğeyi silerken bir hata oluştu.', 'error');  // genel hata mesajı gösteriliyor
                    }
                });
            }
        });
    });
})