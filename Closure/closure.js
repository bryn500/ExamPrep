function makeFunc() {
    var i = 0;

    function displayValue() {
        console.log(i);
        i++;
    }

    i++;

    return displayValue;
}


var myFunc1 = makeFunc();
// scope of makeFunc is over
// i is out of scope?
myFunc1(); //1
myFunc1(); //2
myFunc1(); //3

// No
// A closure is the combination of a function and the lexical environment within which that function was declared.
// This environment consists of any local variables that were in scope at the time the closure was created.

function makeAdder(x) {
    return function (y) {
        return x + y;
    };
}

var add5 = makeAdder(5);
var add10 = makeAdder(10);

console.log(add5(2));  // 7
console.log(add10(2)); // 12


function makeSizer(size) {

    console.log(size);

    return function () {
        document.body.style.fontSize = size + 'px';
    };
}

var size12 = makeSizer(12);
var size14 = makeSizer(14);
var size16 = makeSizer(16);
var size18 = makeSizer(18);


// from https://developer.mozilla.org/en-US/docs/Web/JavaScript/Closures
