function formatMoney(str) {
    return str.split('').reverse().reduce((prev, next, index) => {
        return ((index % 3) ? next : (next + ',')) + prev
    })
}

const addToCart = async (id, quantity) => {
    if (Number(quantity) === 0) {
        toastr.error("Không thể thêm sản phẩm vào giỏ hàng", "Thông báo");
        return;
    }
    const response = await fetch(`/Product/Index/${id}`);
    const data = await response.json();
    if (data !== "NOT_FOUND") {
        let productIds = localStorage.getItem("productIds");
        if (!productIds) {
            productIds = `${id}`;
        } else {
            let temp = productIds + `,${id}`;
            const count = temp.split(",").filter(e => e == id).length;
            if (count <= Number(quantity)) {
                productIds += `,${id}`;
            } else {
                toastr.error("Số lượng sản phẩm đã đạt mức tối đa", "Thông báo");
                return;
            }
        }

        localStorage.setItem("productIds", productIds);
        fetchCart(data, 'plus');
    }
}

const fetchCart = async (data, type) => {
    const productIds = localStorage.getItem("productIds");
    let amount = localStorage.getItem("amount");
    if (data) {
        if (type === 'plus') {
            amount = Number(amount) + Number(data.price);
            localStorage.setItem("amount", amount);
        } else {
            amount = Number(amount) - Number(data.price);
            localStorage.setItem("amount", amount);
        }

    }

    if (amount) {
        document.getElementById("total-amount").innerText = `${formatMoney(localStorage.getItem("amount"))} vnđ`;
    }

    const ids = productIds?.split(",");
    const response = await fetch(`/Product/List?ids=${ids?.toString()}`);
    const products = await response.json();
    if (products !== "NOT_FOUND") {
        document.getElementById("hdk-count").innerText = products.length;
    }
}

fetchCart();



