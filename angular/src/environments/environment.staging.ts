import { Environment } from '@abp/ng.core';

const baseUrl = 'https://staging.oakell.com/';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Oakell',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://localhost:44380/',
    redirectUri: baseUrl,
    clientId: 'Oakell_App',
    responseType: 'code',
    scope: 'offline_access Oakell',
    requireHttps: false,
  },
  apis: {
    default: {
      url: 'http://localhost:44380',
      rootNamespace: 'Oakell',
    },
  },
} as Environment;
