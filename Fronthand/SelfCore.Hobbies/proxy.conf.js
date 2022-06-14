/**
 * For more configuration, please refer to https://angular.io/guide/build#proxying-to-a-backend-server
 *
 * 更多配置描述请参考 https://angular.cn/guide/build#proxying-to-a-backend-server
 *
 * Note: The proxy is only valid for real requests, Mock does not actually generate requests, so the priority of Mock will be higher than the proxy
 */

module.exports = {
    "/api": {
        "target": "http://localhost:5000/api",
        "secure": false,
        "logLevel": "debug",
        "changeOrigin": true,
        "pathRewrite": {
          "^/api": ""
        }
      }
};
