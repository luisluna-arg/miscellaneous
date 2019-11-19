let test1 = '-> a b';
let splitted = test1.split(' ');

let result = splitted.length > 1 ? splitted[1] + ' ' + splitted[0] : '';

result += splitted.length > 2 ? ' ' + splitted[2] : '';

document.getElementById("content").innerHTML  = result;
