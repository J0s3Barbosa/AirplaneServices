const proxy = [
    {
      context: '/api/*',
      target: 'https://localhost:44344',
      pathRewrite: {'^/api' : ''},
      secure : false
    }
  ];
  module.exports = proxy;
