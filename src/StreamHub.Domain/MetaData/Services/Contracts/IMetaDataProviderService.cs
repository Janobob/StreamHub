using StreamHub.Common.Enums;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Services.Contracts;

/// <summary>
///     Service interface for metadata provider services.
/// </summary>
public interface IMetaDataProviderService
{
    /// <summary>
    ///     Gets the name of the metadata provider.
    /// </summary>
    string Name { get; }

    #region Metadata

    /// <summary>
    ///     Retrieves metadata for a media item by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the media item.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an instance of
    ///     <see cref="MediaMetaData" /> if the media item is found; otherwise, <c>null</c>.
    /// </returns>
    Task<Result<MediaMetaData?>> GetMediaMetaDataAsync(int id);

    /// <summary>
    ///     Retrieves metadata for a movie by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the movie.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an instance of
    ///     <see cref="MovieMetaData" /> if the movie is found; otherwise, <c>null</c>.
    /// </returns>
    Task<Result<MovieMetaData?>> GetMovieMetaDataAsync(int id);

    /// <summary>
    ///     Retrieves metadata for a series by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the series.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an instance of
    ///     <see cref="SeriesMetaData" /> if the series is found; otherwise, <c>null</c>.
    /// </returns>
    Task<Result<SeriesMetaData?>> GetSeriesMetaDataAsync(int id);

    #endregion

    #region Search

    /// <summary>
    ///     Searches for media items of a specific type based on the provided query.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="limit">The maximum number of search results to return. Default is 10.</param>
    /// <param name="type">The type of media to search for (e.g., Movie, Series).</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an
    ///     <see cref="IEnumerable{T}" /> of <see cref="MetaDataSearchResult" /> representing the search results.
    /// </returns>
    Task<Result<IEnumerable<MetaDataSearchResult>>> SearchMediaAsync(string query,
        int limit, MediaType type = MediaType.All);

    /// <summary>
    ///     Searches for movies based on the provided query.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="limit">The maximum number of search results to return. Default is 10.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an
    ///     <see cref="IEnumerable{T}" /> of <see cref="MetaDataSearchResult" /> representing the search results.
    /// </returns>
    Task<Result<IEnumerable<MetaDataSearchResult>>> SearchMoviesAsync(string query, int limit);

    /// <summary>
    ///     Searches for series based on the provided query.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="limit">The maximum number of search results to return. Default is 10.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an
    ///     <see cref="IEnumerable{T}" /> of <see cref="MetaDataSearchResult" /> representing the search results.
    /// </returns>
    Task<Result<IEnumerable<MetaDataSearchResult>>> SearchSeriesAsync(string query, int limit);

    #endregion
}