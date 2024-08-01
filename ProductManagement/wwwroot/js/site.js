// Utility functions
const query = sel => document.querySelector(sel);
const queryAll = sel => document.querySelectorAll(sel);
const onEvent = (elt, evt, handler) => elt.addEventListener(evt, handler);
const log = console.log;
const danger = () => window.confirm("Are you sure you want to delete this record?");

const deleteItem = path => {
    query(".btn-proceed").href = path;
    return false;
};
let iid = 0;
let currentVal = 0;
function setTimer(callback, delay = 700) {
    clearTimeout(iid);
    iid = setTimeout(callback, delay);
}
function toggleBtns(id, state) {
    const elts = [
        query(`.btn-quantity-group[data-id="${id}"] .txt`),
        query(`.btn-quantity-group[data-id="${id}"] .btn-add`),
        query(`.btn-quantity-group[data-id="${id}"] .btn-min`)
    ];
    elts[0].disabled = state;
    elts.forEach(el => el.classList.toggle("disabled"));
}
function submit(id, quantity) {
    clearTimeout(iid);
    log(id, quantity);
    toggleBtns(id, true);
    fetch(`/Product/ChangeQuantity/${id}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(quantity)
    }).then(res => {
        if (res.ok) 
            return res.json();
    }).then(data => {
        toggleBtns(id, false);
        query(`.btn-quantity-group[data-id="${id}"] .txt`).value = data.quantity;
    }).catch(err => log);
}
function changeQuantity(id, txt, step) {
    const val = Math.max(parseInt(txt.value) + step, 0);
    txt.value = val;
    setTimer(() => submit(id, val));
}
function inscQuantity(id, txt) {
    changeQuantity(id, txt, 1);
}
function descQuantity(id, txt) {
    changeQuantity(id, txt, -1);
}
function initButtons(p) {
    const id = parseInt(p.getAttribute("data-id"));
    const addBtn = p.querySelector(".btn-add");
    const minBtn = p.querySelector(".btn-min");
    const txt = p.querySelector(".txt");

    onEvent(addBtn, "click", () => inscQuantity(id, txt));
    onEvent(minBtn, "click", () => descQuantity(id, txt));

    onEvent(txt, "keyup", evt => {
        if (evt.code === 'Enter') {
            txt.blur();
        }
    });

    onEvent(txt, "focus", () => {
        currentVal = Math.max(parseInt(txt.value), 0);
    });

    onEvent(txt, "blur", () => {
        const val = Math.max(parseInt(txt.value), 0);
        if (val != currentVal)
            submit(id, val);
    });
}
function initQuantityButtons() {
    const products = queryAll(".btn-quantity-group");
    products.forEach(initButtons);
}

initQuantityButtons();


// products
queryAll(".product").forEach(p => {
    onEvent(p, "click", evt => {
        p.classList.toggle("selected");
    });
});