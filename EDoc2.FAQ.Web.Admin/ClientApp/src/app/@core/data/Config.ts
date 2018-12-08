const API_BASE_URL = "http://localhost:5000/api/v1";

export const Apis = {
    Endpoint: API_BASE_URL.toString(),
    Authorize: "/Authorize/token",
    Register: "/Account/register",
    Logout: "/Account/logout",
    ForgetPass: "/Account/forget",
    ResetPass: "/Account/reset",
    AccessToken: "",
    AccessTokenName: "access_token",
    SearchUsers: API_BASE_URL.toString() + "/Admin/searchUsers"
};

export const Roles = {
    Administrator: {
        Normalized: "ADMINISTRATOR"
    },
    Moderator: {
        Normalized: "MODERATOR",
        Pages: [
            "pages/accounts/list",
        ]
    }
};

