const API_BASE_URL = `http://${document.location.hostname}:8080`;

export const Apis = {
    BASE_URL : API_BASE_URL,
    PageSize: 30,
    CommentPageSize: 100,
    Authorize: {
        AuthToken: null,
        UserId: '',
        IsSignIn: false,
        Login: '/api/v1/Authorize/token',
        Register: '/api/v1/Account/register',
        Logout: '/api/v1/Account/logout',
        Forget: '/api/v1/Account/forget',
        Reset: '/api/v1/Account/reset',
    },
    ModifyAvatar: API_BASE_URL + '/api/v1/Account/modifyAvatar',
    AvatarUrl: API_BASE_URL + '/api/v1/Account/avatar/',
    ClaimTypes: {
        Id: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
        Role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
        Email: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    },
    IconScriptUrl: 'https://at.alicdn.com/t/font_985054_iabiqo246jm.js'
};
