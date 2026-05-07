const buttons = document.querySelectorAll(".tab-button");
const sections = document.querySelectorAll(".app-section");

buttons.forEach((button) => {
  button.addEventListener("click", () => {
    const { target } = button.dataset;

    buttons.forEach((item) => item.classList.remove("active"));
    sections.forEach((section) => section.classList.remove("active-section"));

    button.classList.add("active");
    document.getElementById(target)?.classList.add("active-section");
    document.getElementById(target)?.scrollIntoView({ behavior: "smooth", block: "start" });
  });
});
