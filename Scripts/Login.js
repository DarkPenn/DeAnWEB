document.addEventListener("DOMContentLoaded", function () {
    //  GIỚI HẠN ĐỘ DÀI INPUT (KHÓA KHÔNG CHO NHẬP THÊM) 
    function setMaxLength(id, maxLength) {
        const input = document.getElementById(id);
        if (!input) return;

        input.addEventListener("input", function () {
            if (this.value.length > maxLength) {
                this.value = this.value.slice(0, maxLength);
            }
        });
    }

    // Áp dụng giới hạn độ dài
    setMaxLength("firstName", 14);
    setMaxLength("lastName", 14);
    setMaxLength("registerEmail", 28);
    setMaxLength("registerPassword", 20);
    setMaxLength("email", 28);
    setMaxLength("password", 20);

    //  CHUYỂN GIỮA 2 FORM 
    document.getElementById("showRegister").addEventListener("click", function (e) {
        e.preventDefault();
        document.getElementById("loginForm").style.display = "none";
        document.getElementById("registerForm").style.display = "block";
    });

    document.getElementById("showLogin").addEventListener("click", function (e) {
        e.preventDefault();
        document.getElementById("registerForm").style.display = "none";
        document.getElementById("loginForm").style.display = "block";
    });

    //  KIỂM TRA ĐĂNG NHẬP 
    document.getElementById("loginBtn").addEventListener("click", function () {
        const email = document.getElementById("email").value.trim();
        const password = document.getElementById("password").value.trim();
        const errorDiv = document.getElementById("errorMessage");
        const successDiv = document.getElementById("successMessage");

        errorDiv.style.display = "none";
        successDiv.style.display = "none";

        if (email === "" || password === "") {
            errorDiv.innerHTML = "<ul><li>Vui lòng nhập đầy đủ email và mật khẩu.</li></ul>";
            errorDiv.style.display = "block";
            return;
        }

        // Lấy danh sách user từ localStorage
        let users = JSON.parse(localStorage.getItem("users")) || [];
        let found = users.find(u => u.email === email && u.password === password);

        if (!found) {
            errorDiv.innerHTML = "<ul><li>Email hoặc mật khẩu không đúng.</li></ul>";
            errorDiv.style.display = "block";
            return;
        }

        successDiv.textContent = "Đăng nhập thành công! Xin chào " + found.firstName + " " + found.lastName;
        successDiv.style.display = "block";
    });

    //  KIỂM TRA ĐĂNG KÝ 
    document.getElementById("registerBtn").addEventListener("click", function () {
        const firstName = document.getElementById("firstName").value.trim();
        const lastName = document.getElementById("lastName").value.trim();
        const email = document.getElementById("registerEmail").value.trim();
        const password = document.getElementById("registerPassword").value.trim();
        const errorDiv = document.getElementById("registerMessage");
        const successDiv = document.getElementById("registerSuccess");

        errorDiv.style.display = "none";
        successDiv.style.display = "none";

        if (firstName === "" || lastName === "" || password === "" || email === "") {
            errorDiv.innerHTML = "<ul><li>Vui lòng điền đầy đủ thông tin.</li></ul>";
            errorDiv.style.display = "block";
            return;
        }

        if (!email.endsWith("gmail.com")) {
            errorDiv.innerHTML = "<ul><li>Email phải có dạng gmail.com.</li></ul>";
            errorDiv.style.display = "block";
            return;
        }

        // Lấy user cũ
        let users = JSON.parse(localStorage.getItem("users")) || [];

        // Kiểm tra email đã tồn tại chưa
        let exists = users.find(u => u.email === email);
        if (exists) {
            errorDiv.innerHTML = "<ul><li>Email này đã được đăng ký.</li></ul>";
            errorDiv.style.display = "block";
            return;
        }

        // Thêm user mới
        let newUser = { firstName, lastName, email, password };
        users.push(newUser);
        localStorage.setItem("users", JSON.stringify(users));

        successDiv.textContent = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
        successDiv.style.display = "block";

        // Xóa form
        document.getElementById("firstName").value = "";
        document.getElementById("lastName").value = "";
        document.getElementById("registerEmail").value = "";
        document.getElementById("registerPassword").value = "";
    });

});