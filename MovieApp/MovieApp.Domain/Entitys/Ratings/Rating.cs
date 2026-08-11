
using MoviApp.SharedKernel.Entitys;

namespace MovieApp.Domain.Entitys.Ratings
{
    public sealed class Rating : Entity
    {
        public int Score { get; private set; }
        public Guid MovieId { get; private set; }
        public Guid UserId { get; private set; }

        private Rating(
            Guid id, Guid movieId,Guid userId,int score):base(id)
        {
            MovieId = movieId;
            UserId = userId;
            Score = score;
        }

        public static Rating Create(
            Guid movieId,Guid userId,int score)
        {
            var rating = new Rating(
                Guid.NewGuid(),movieId,userId,score);

            return rating;

        }

        public void Update(int  score) 
        { 
            Score = score;
        }
    }
}
