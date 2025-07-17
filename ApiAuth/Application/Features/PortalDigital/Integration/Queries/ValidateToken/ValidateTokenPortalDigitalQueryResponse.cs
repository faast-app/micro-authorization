namespace ApiAuth.Application.Features.PortalDigital.Integration.Queries.ValidateToken;

public sealed record ValidateTokenPortalDigitalQueryResponse(bool TokenIsValid, string numeroDocumentoCliente, string nombreCliente, string tipoOferta);