using Microsoft.EntityFrameworkCore;


namespace ProjetSessionAppWeb3.Models
{
    /// <summary>
    /// Classe servant à créer notre base de donnée
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext() { }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserChat> UserChats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;user id=root;database=projetwebapp3;port=3306");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasKey(m => m.IdMessage)
                .HasName("PRIMARY");

                entity.HasIndex(m => m.IdUser, "FK_UserMessageConstraint");

                entity.HasIndex(m => m.IdChat, "FK_ChatMessageConstraint");

                entity.Property(m => m.IdMessage)
                .HasColumnName("idMessage")
                .IsRequired();

                entity.Property(m => m.IdUser)
                .HasColumnName("idUser")
                .IsRequired();

                entity.Property(m => m.IdChat)
                .HasColumnName("idChat")
                .IsRequired();

                entity.Property(m => m.Username)
                .HasColumnName("username")
                .HasMaxLength(25)
                .IsRequired();

                entity.Property(m => m.DateMessaged)
                .HasColumnName("dateMessaged")
                .IsRequired();

                entity.Property(m => m.Description)
                .HasColumnName("description")
                .IsRequired();

                //Les relations de Message

                entity.HasOne<User>(m => m.IdUserReference)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.IdUser)
                .HasConstraintName("FK_UserMessageConstraint");
                //.OnDelete(DeleteBehavior.SetNull);

                entity.HasOne<Chat>(m => m.IdChatReference)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.IdChat)
                .HasConstraintName("FK_ChatMessageConstraint");
                //.OnDelete(DeleteBehavior.SetNull);

            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("chat");

                entity.HasKey(c => c.IdChat)
                .HasName("PRIMARY");

                entity.HasIndex(c => c.IdCreator, "FK_ChatCreatorConstraint");

                entity.Property(c => c.IdChat)
                .HasColumnName("idChat")
                .IsRequired();

                entity.Property(c => c.ChatName)
                .HasColumnName("chatName")
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(c => c.IdCreator)
                .HasColumnName("idCreator")
                .IsRequired();

                //Relation chat a un creator, mais creator peut avoir plusieurs chat
                entity.HasOne(c => c.IdCreatorReference)
                .WithMany(u => u.Chats)
                .HasForeignKey(c => c.IdCreator)
                .HasConstraintName("FK_ChatCreatorConstraint");
                //.OnDelete(DeleteBehavior.SetNull);

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasKey(u => u.IdUser)
                .HasName("PRIMARY");

                entity.Property(u => u.IdUser)
                .HasColumnName("idUser")
                .IsRequired();

                entity.Property(u => u.Username)
                .HasColumnName("username")
                .HasMaxLength(30)
                .IsRequired();

                entity.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(70)
                .IsRequired();

                entity.Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(60)
                .IsRequired();

            });

            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.ToTable("userchat");

                entity.HasKey(uc => new { uc.IdUser, uc.IdChat })
                .HasName("PRIMARY");

                entity.HasIndex(uc => uc.IdUser, "FK_UserId");

                entity.HasIndex(uc => uc.IdChat, "FK_ChatId");

                entity.Property(uc => uc.IdUser)
                .HasColumnName("idUser")
                .IsRequired();

                entity.Property(ue => ue.IdChat)
                .HasColumnName("idchat")
                .IsRequired();

                entity.HasOne(uc => uc.IdUserReference)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.IdUser)
                .HasConstraintName("FK_UserId");

                entity.HasOne(uc => uc.IdChatReference)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.IdChat)
                .HasConstraintName("FK_ChatId");


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modeluBuilder);

    }
}
