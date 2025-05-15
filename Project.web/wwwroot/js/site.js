function InputOnlyNumber(input) {
  input.value = input.value.replace(/[^0-9]/g, "");
}

function ShowPassword() {
  const togglePassword = document.getElementById("togglePassword");
  const password = document.getElementById("password");
  const type =
    password.getAttribute("type") === "password" ? "text" : "password";

  if (type === "text") {
    togglePassword.setAttribute("class", "fa fa-eye-slash");
  } else {
    togglePassword.setAttribute("class", "fa fa-eye");
  }

  password.setAttribute("type", type);
}

function InputOnlyNumber(input) {
  input.value = input.value.replace(/[^0-9]/g, "");
}

function InputDecimalNumber(input) {
  input.value = input.value
    .replace(/[^0-9.]/g, "")
    .replace(/^([^.]*\.)|\./g, "$1");
}
