namespace Api.Auxiliaries.Constants;

static class Patterns
{
    internal const string Plate = "^[a-z0-9 ]{,7}$";
    internal const string Cid = "^(19|20)?[\\d]{6}[-]?[\\d]$";
    internal const string Guid = "^[0-9a-f]{8}([-]?[0-9a-f]{4}){4}[0-9a-f]{8}$";
    internal const string Occasion = "yyy-MM-dd HH:mm";
}