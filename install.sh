#!/bin/bash

PASS=crypticpassword
CERT_DIR=https

WEB_ADMIN_DIR=frontend/apps/admin/https
WEB_STORE_DIR=frontend/apps/store/https

GATEWAY_DIR=backend/Gateway/https
CATALOG_SERVICE_DIR=backend/Services/CatalogService/CatalogService.API/Certificates
IDENTITY_SERVICE_DIR=backend/Services/IdentityService/IdentityService.API/Certificates

openssl genrsa -out ${CERT_DIR}/ca.key 4096

openssl req -config ${CERT_DIR}/config/certificate.conf \
 -x509 -new -key ${CERT_DIR}/ca.key -sha256 -days 365 \
 -out ${CERT_DIR}/ca.cert 

openssl genrsa -out ${CERT_DIR}/server.key 4096

openssl req -new -key ${CERT_DIR}/server.key \
 -out ${CERT_DIR}/server.csr \
 -config ${CERT_DIR}/config/certificate.conf


openssl x509 -req -in ${CERT_DIR}/server.csr \
 -CA ${CERT_DIR}/ca.cert \
 -CAkey ${CERT_DIR}/ca.key \
 -CAcreateserial -out ${CERT_DIR}/server.crt \
 -days 365 -sha256 -extfile ${CERT_DIR}/config/certificate.conf -extensions req_ext

echo Done! Copying files to folders...
sleep 1



cp ${CERT_DIR}/server.crt ${WEB_ADMIN_DIR}/server.crt
cp ${CERT_DIR}/server.key ${WEB_ADMIN_DIR}/server.key

cp ${CERT_DIR}/server.crt ${WEB_STORE_DIR}/server.crt
cp ${CERT_DIR}/server.key ${WEB_STORE_DIR}/server.key


cp ${CERT_DIR}/server.crt ${GATEWAY_DIR}/server.crt
cp ${CERT_DIR}/server.key ${GATEWAY_DIR}/server.key


bash -c "dotnet dev-certs https -ep ${CATALOG_SERVICE_DIR}/CatalogService.pfx -p ${PASS}"
bash -c "dotnet dev-certs https -ep ${IDENTITY_SERVICE_DIR}/IdentityService.pfx -p ${PASS}"



echo Done! Closing in 3 seconds...
sleep 3