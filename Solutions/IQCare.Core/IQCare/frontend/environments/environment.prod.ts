

export const environment = {
    production: true,
    API_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCareAPI',
    API_LAB_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCareLab',
    API_PMTCT_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCarePMTCT',
    API_AIR_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCareAIR',
    API_PREP_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCarePREP',
    API_QUEUE_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCareQueue'
    API_PHARM_URL: location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/IQCarePHARM'
};
