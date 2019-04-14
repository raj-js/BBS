const API_BASE_URL = 'http://192.168.251.72:8080/api/v1';

export const Apis = {
    Endpoint: API_BASE_URL.toString(),
    Authorize: '/Authorize/token',
    Register: '/Account/register',
    Logout: '/Account/logout',
    ForgetPass: '/Account/forget',
    ResetPass: '/Account/reset',
    AccessToken: '',
    AccessTokenName: 'access_token',
    SearchUsers: API_BASE_URL.toString() + '/Admin/searchUsers',
    SearchArticles: API_BASE_URL.toString() + '/Article/search',
};

export const Roles = {
    Administrator: {
        Normalized: 'ADMINISTRATOR',
    },
    Moderator: {
        Normalized: 'MODERATOR',
        Pages: [
            'pages/accounts/list',
        ],
    },
};

