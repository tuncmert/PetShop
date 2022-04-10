
$(document).ready(function () {
    var cartItems = JSON.parse(localStorage.getItem("cartItems"));
    if (!cartItems) {
        localStorage.setItem("cartItems", JSON.stringify([]));
    }
    else {
        $('#shopingCartItemCount').html(UrunSayisiniHesapla(cartItems));
        $('#shopingCartItemPrice').html(UrunFiyatiHesapla(cartItems));


    }

    $('a[id^="product"]').click(function (e) {
        //e event'i temsil eder. 
        //preventDefault() fonksiyonu eventi geçersiz kılar. Yani varsayılan özelliğini yitirtir.
        e.preventDefault();
        cartItems = JSON.parse(localStorage.getItem("cartItems"));
        var productId = $(this).attr('id').substr(8);
        var productName = $(this).data('productname');
        var productPrice = parseInt($(this).data('productprice'));
        var quantity = 1;

        var varolanUrunler = cartItems.filter(p => p.productId == productId)[0];
        if (varolanUrunler) {
            varolanUrunler.quantity = Number(varolanUrunler.quantity) + 1;
            console.log("Bu ürün var", varolanUrunler);
            //return;
        }
        else {
            var product = { "productId": productId, "quantity": quantity, "productName": productName, "productPrice": productPrice }
            cartItems.push(product)
        }
        var toplamUrunSayisi = UrunSayisiniHesapla(cartItems);
        var toplamUrunFiyati = UrunFiyatiHesapla(cartItems);
        console.log(toplamUrunFiyati);
        $('#shopingCartItemCount').html(toplamUrunSayisi);
        $('#shopingCartItemPrice').html(toplamUrunFiyati);
        



        localStorage.setItem("cartItems", JSON.stringify(cartItems));
    });
})
function UrunSayisiniHesapla(cartItems) {
    var toplamUrunSayisi = 0;
    cartItems.map(p => {
        toplamUrunSayisi += p.quantity
    })
    return toplamUrunSayisi;
}
function UrunFiyatiHesapla(cartItems) {
    var toplamUrunFiyati = 0;
    cartItems.map(p => {
        toplamUrunFiyati += (p.productPrice*p.quantity)
    })
    return toplamUrunFiyati;
}
function btnUrunFilter(e) {
    console.log(e);
    var kategoriId   = $(e).data('kategori');
    var obj = { KategoriId: kategoriId };
    console.log(obj);
    $.ajax({
        url: '/Home/UrunFiltre',
        type: 'post',
        dataType: 'JSON',
        data: JSON.stringify(obj),
        contentType: 'application/json;charset=utf-8',
        success: function (results) {
            if (results == 0) {
                alert("Ürün bulunamadı!")
            }
            $('#productsContainer div').each(function () { $(this).remove() });
            for (var i = 0; i < results.length; i++) {
                var product = results[i];
                console.log(product);
                var element = $(`
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="product__item">
                            <div class="product__item__pic set-bg">
                            <img src='${product.urunResim1==null?"/images/NoImage2.png":"data:image/png;base64,"+product.urunResim1}'>
                                <ul class="product__item__pic__hover">
                                    <li><a href="" id="product-${product.urunId}" data-productname="${product.urunAd}" data-productprice="${product.urunFiyat}"><i class="fa fa-shopping-cart"></i></a></li>
                                    <li>
                                        <a href="/Home/UrunDetay/${product.urunId}"><i class="fa fa-list-alt"></i></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="product__item__text">
                                <h6><a href="#">${product.urunAd}</a></h6>
                                <h5>$${product.urunFiyat}</h5>
                            </div>
                        </div>
                    </div>`);
                $('#productsContainer').append(element);
            }
        }
    })
}
