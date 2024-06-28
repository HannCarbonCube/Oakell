import { Environment } from '@abp/ng.core';

const baseUrl = 'https://staging.oakell.com/';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Oakell',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://api.staging.oakell.com/',
    clientId: 'Oakell_App',
    scope: 'offline_access Oakell'
  },
  apis: {
    default: {
      url: 'https://api.staging.oakell.com/',
      rootNamespace: 'Oakell',
    },
  },
} as Environment;
