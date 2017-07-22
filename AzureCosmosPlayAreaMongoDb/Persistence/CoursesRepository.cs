using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using AzureCosmosPlayAreaMongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace AzureCosmosPlayAreaMongoDb.Persistence
{
    public class CoursesRepository
    {
        private readonly DbContext _dbContext;

        public CoursesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course GetCourse(Guid id)
        {
            IMongoCollection<Course> collection = GetCollection();

            Course course = collection.AsQueryable().FirstOrDefault(c => c.Id.Equals(id));

            return course;
        }

        public List<Course> GetCourses()
        {
            IMongoCollection<Course> collection = GetCollection();

            return collection.Find(new BsonDocument()).ToList();
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            IMongoCollection<Course> collection = GetCollection();

            await collection.InsertOneAsync(course);

            return course;
        }

        public async Task<Course> UpdateCourseAsync(Guid id, Course course)
        {
            IMongoCollection<Course> collection = GetCollection();

            await collection.ReplaceOneAsync(c => c.Id.Equals(id), course);

            return course;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            IMongoCollection<Course> collection = GetCollection();

            await collection.DeleteOneAsync(c => c.Id.Equals(id));
        }

        private IMongoCollection<Course> GetCollection()
        {
            return _dbContext.Database.GetCollection<Course>(_dbContext.CollectionName);
        }
    }
}