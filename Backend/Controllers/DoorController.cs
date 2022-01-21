using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Api.DBManagement;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorController : ControllerBase
    {
        private ReaderWriterLockSlim DBLock = new ReaderWriterLockSlim();
        // GET: api/Door
        
        [HttpGet]
        public IEnumerable<Door> Get()
        {
            try
            {
                DBLock.EnterReadLock();
                var entry = new List<Door>();
                using (DBAdapter db = new DBAdapter())
                {
                    entry = db.Read().ToList();
                }

                return entry;
            }
            catch (Exception ex)
            {
                //TODO log the errors
                throw ex;
            }
            finally
            {
                DBLock.ExitReadLock();
            }
        }

        // GET: api/Door/5
        [HttpGet("{id}", Name = "Get")]
        public Door Get([FromQuery] int id)
        {
            try
            {
                DBLock.EnterReadLock();
                var entry = new Door();
                using (DBAdapter db = new DBAdapter())
                {
                    entry = db.Read(id);
                }

                return entry;
            }
            catch (Exception ex)
            {
                //TODO log the errors
                throw ex;
            } 
            finally
            {
                DBLock.ExitReadLock();
            }
        }

        // POST: api/Door
        [HttpPost]
        public void Post([FromBody] Door door)
        {
            try
            {
                DBLock.EnterWriteLock();
                using (DBAdapter db = new DBAdapter())
                {
                    db.Create(door);
                }
            }
            catch (Exception ex)
            {
                //TODO log the errors
                throw ex;
            } 
            finally
            {
                DBLock.ExitWriteLock();
            }
        }

        // PUT: api/Door/5
        [HttpPut]
        public void Put([FromBody] Door door)
        {
            try
            {
                DBLock.EnterWriteLock();
                using (DBAdapter db = new DBAdapter())
                {
                    db.Update(door);
                }
            }
            catch (Exception ex)
            {
                //TODO log the errors
                throw ex;
            } 
            finally
            {
                DBLock.ExitWriteLock();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                DBLock.EnterWriteLock();
                using (DBAdapter db = new DBAdapter())
                {
                    db.Delete(id);
                }
            }
            catch (Exception ex)
            {
                //TODO log the errors
                throw ex;
            }
            finally
            {
                DBLock.ExitWriteLock();
            }
        }
    }
}
