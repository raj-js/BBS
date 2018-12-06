const API_BASE_URL = "http://localhost:5000/api/v1";

export const Apis = {
    Endpoint: API_BASE_URL.toString(),
    Authorize: "/Token",
    Register: "/Account/register",
    Logout: "/Account/logout",
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

