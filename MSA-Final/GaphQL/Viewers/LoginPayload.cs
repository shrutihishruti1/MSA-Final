

using MSA_Final.Models;

namespace MSA_Final.GaphQL.Viewers
{
    public record LoginPayload(
        Viewer viewer,
        string jwt);
}