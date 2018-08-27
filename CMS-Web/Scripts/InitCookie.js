﻿var CookieName = "cms-order";
function CreateOrUpdateOrder(objOrder)
{
    var Orders = getListOrder();
    if (Orders != null)
    {
        if (Orders.map(function (o) { return o.ItemId }).indexOf(objOrder.ItemId) == -1) {
            // add order
            Orders.push(objOrder);
        }
        else {
            $.each(Orders, function (index, item) {
                if (item.ItemId === objOrder.ItemId) {
                    // update quantity
                    item.Quantity = parseInt(item.Quantity) + parseInt(objOrder.Quantity);
                }
            });
        }

        // remove cookie current
        Cookies.remove(CookieName);
        //add new cookie
        Cookies.set(CookieName, Orders, { expires: 1 });
    }
    else {
        var _order = [];
        _order.push(objOrder);
        Cookies.set(CookieName, _order, { expires: 1 })
    }

    //get list order again
    Orders = getListOrder();
}

function getListOrder()
{
    var Orders = null;
    var _Order = Cookies.getJSON(CookieName)
    if (_Order != undefined)
    {
        Orders = _Order;
        if (Orders !== undefined && Orders !== null) {
            var _Quantity = 0;
            var _Price = 0;
            $.each(Orders, function (index, item) {
                _Quantity = _Quantity + item.Quantity;
                _Price = parseFloat(_Price) + parseFloat(item.Price * item.Quantity);
            });
            _Price = parseFloat(_Price, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").replace(".00", "").toString() + " đ";
            $('#top-cart-count').text("Giỏ hàng [" + _Quantity+"]");
            $('#top-cart-total .amount').text(_Price)
           
        }
    }
    return Orders;
}

function deleteOrder() {
    Cookies.remove(CookieName);
    $('#top-cart-count').text(0);
}

function UpdateQuantity(itemId, qty)
{
    var Orders = getListOrder();
    if (Orders != null) {
        $.each(Orders, function (index, item) {
            if (item.ItemId === itemId) {
                // update quantity
                item.Quantity = qty;
            }
        });
        // remove cookie current
        Cookies.remove(CookieName);
        //add new cookie
        Cookies.set(CookieName, Orders, { expires: 1 });
    }
}

function RemoveOrderByItemId(itemId)
{
    var Orders = getListOrder();
    if (Orders != null)
    {
        var flgRemoveCookie = false;
        for (var i = 0; i < Orders.length; i++) {
            if (Orders[i].ItemId === itemId) {
                Orders.splice(i, 1);
                flgRemoveCookie = true;
            }
        }
        if (flgRemoveCookie) {
            Cookies.remove(CookieName);
            Cookies.set(CookieName, Orders);
        } 
    }
}

function RegexEmail(Email) {
    var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;
    if (!pattern.test(Email))
        return false;
    return true;
}