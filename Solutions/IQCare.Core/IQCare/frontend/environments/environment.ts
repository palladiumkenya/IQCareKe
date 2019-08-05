// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
    production: false,
    API_URL: location.protocol + '//localhost:3333',
    API_LAB_URL: location.protocol + '//localhost:5000',
    API_PMTCT_URL: location.protocol + '//localhost:56486',
    API_AIR_URL: location.protocol + '//localhost:44398',
    API_PREP_URL: location.protocol + '//localhost:50666'
};
