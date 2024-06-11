import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Oakell',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://localhost:44380/',
    clientId: 'Oakell_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'offline_access Oakell',
  },
  apis: {
    default: {
      url: 'http://localhost:44380',
      rootNamespace: 'Oakell',
    },
  },
} as Environment;
